import { Component,inject } from '@angular/core';
import { RouterOutlet, Router } from '@angular/router';
import { HeaderComponent } from './components/header/header.component';
import { FooterComponent } from './components/footer/footer.component';
import { SessionService } from './services/session/session.service';

@Component({
  selector: 'app-root',
  imports: [
    RouterOutlet,
    HeaderComponent,
    FooterComponent
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  readonly title = 'app';

  private readonly _sessionService: SessionService = inject(SessionService);

  private readonly _router: Router = inject(Router);

  public constructor() {
    this._sessionService.validate$.subscribe(result => {
      if (result.valid) {
        this._router.navigate(['scrape']);
      }
      else {
        this._router.navigate(['login']);
      }
    });
  }

}
