import { Component, signal, inject } from '@angular/core';
import { AsyncPipe } from '@angular/common';
import { SessionService } from '../../services/session/session.service';
import { Router } from '@angular/router';
import { ApiService } from '../../api/api.service';
import { loginLogoutPost } from '../../api/fn/login/login-logout-post';
import { switchMap, tap } from 'rxjs/operators';

@Component({
  selector: 'app-header',
  imports: [AsyncPipe],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {

  private readonly _apiService: ApiService = inject(ApiService);
  protected readonly _sessionService: SessionService = inject(SessionService);
  private readonly _router: Router = inject(Router);


  protected login() {
    this._router.navigate(['login']);
  }

  protected report() {
    this._router.navigate(['report']);
  }

  protected scrape() {
    this._router.navigate(['scrape']);
  }

  protected logout() {
    this._apiService
      .invoke(loginLogoutPost)
      .pipe
      (
        switchMap(result => this._sessionService.validate$),
        tap(result => this._sessionService.loggedIn$.next(result.valid ?? false)),
      )
      .subscribe(
        result => {
          if (!result.valid) {
            this._router.navigate(['login']);
          }

        }
      );
  }

}
