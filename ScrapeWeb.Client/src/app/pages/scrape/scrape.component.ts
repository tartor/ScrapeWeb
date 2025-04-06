import { Component, signal, viewChild, inject } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { ApiService } from '../../api/api.service';
import { scrapeScrapePost } from '../../api/fn/scrape/scrape-scrape-post';
import { Router } from '@angular/router';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
import { handleError } from '../../utils/error-handler';

@Component({
  selector: 'app-scrape',
  imports: [
    FormsModule
  ],
  templateUrl: './scrape.component.html',
  styleUrl: './scrape.component.css'
})
export class ScrapeComponent {

  private readonly _form = viewChild.required(NgForm);

  protected readonly isProcessing = signal(false);
  protected readonly selector = signal<string | null>('land registry searches');
  protected readonly url = signal<string | null>('www.infotrack.co.uk');

  protected readonly error = signal<string | null>('');

  protected readonly scrape = signal<SafeHtml | null>('');

  private readonly _apiService: ApiService = inject(ApiService);
  private readonly _router: Router = inject(Router);

  private readonly _sanitizer: DomSanitizer = inject(DomSanitizer);

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
        scrapeScrapePost,
        {
          body: { ...form.value },
        }
      )
      .subscribe({
        next: async (result) => {
          if (result.error) {
            this.error.set(result.error ?? "");
            return;
          }

          if ((result.item?.place ?? 0) <= 0) {
            const escapedHtml = this._sanitizer.bypassSecurityTrustHtml(
              `The URL <b>${this.url()}</b> is not present in the search results for <b>${this.selector()}</b>`
            );
            this.scrape.set(escapedHtml);
          }
          else {
            const escapedHtml = this._sanitizer.bypassSecurityTrustHtml(
              `The URL <b>${this.url()}</b> appears in position <b>${result.item?.place}</b> when searching for <b>${this.selector()}</b>`
            );
            this.scrape.set(escapedHtml);
          }
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
