import { Component, signal, viewChild, inject } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { SessionService } from '../../services/session/session.service';
import { ApiService } from '../../api/api.service';
import { loginLoginPost } from '../../api/fn/login/login-login-post';
import { Router } from '@angular/router';
import { handleError } from '../../utils/error-handler';
@Component({
  selector: 'app-login',
  imports: [
    FormsModule
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  private readonly _form = viewChild.required(NgForm);

  protected readonly isProcessing = signal(false);
  protected readonly username = signal<string | null>('John');
  protected readonly password = signal<string | null>('password123');
  protected readonly error = signal<string | null>('');

  private readonly _apiService: ApiService = inject(ApiService);
  private readonly _sessionService: SessionService = inject(SessionService);
  private readonly _router: Router = inject(Router);

  protected onSubmit() {
    const form = this._form();
    this.error.set("");

    if (form.status !== 'VALID') {
      this.error.set("Fill in the form");
      return;
    }

    this.isProcessing.set(true);

    this._apiService
      .invoke(
        loginLoginPost,
        {
          body: { ...form.value },
        }
      )
      .subscribe({
        next: async (result) => {
          if (!result.success) {
            this.error.set(result.error ?? "");
            return;
          }

          this._sessionService.loggedIn$.next(true);
          await this._router.navigate(['scrape']);
        },
        error: (err) => {
          this.isProcessing.set(false);
          handleError(form, err, this.error);
        },
        complete: () => {
          this.isProcessing.set(false);
        },
      });
  }

}
