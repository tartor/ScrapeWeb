import { Component, inject, signal, viewChild } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { DatePipe } from '@angular/common';
import { ApiService } from '../../api/api.service';
import { reportReportPost } from '../../api/fn/report/report-report-post';
import { ScrapeItem } from '../../api/models/scrape-item';
import { PlacePipe } from '../../pipes/place/place.pipe';
import { handleError } from '../../utils/error-handler';

@Component({
  selector: 'app-report',
  imports: [
    FormsModule,
    DatePipe,
    PlacePipe
  ],
  templateUrl: './report.component.html',
  styleUrl: './report.component.css'
})
export class ReportComponent {

  private readonly _form = viewChild.required(NgForm);

  protected readonly from = signal<string | null>(null);
  protected readonly to = signal<string | null>(null);

  protected readonly isProcessing = signal(false);

  private readonly _apiService: ApiService = inject(ApiService);
  protected items = signal<Array<ScrapeItem> | null>([]);
  protected readonly error = signal<string | null>('');


  private nullIfEmpty(v: string | null) {
    if (v)
      return v;
    return null;
  }
   

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
        reportReportPost,
        {
          body: { from: this.nullIfEmpty(this.from()), to: this.nullIfEmpty(this.to()) }
        }
       )
      .subscribe({
        next: async (result) => {
          this.isProcessing.set(false);
          this.items.set(result.items ?? []);
        },
        error: (err) => {
          this.isProcessing.set(false);
          handleError(null, err, this.error);
        },
        complete: () => {
          this.isProcessing.set(false);
        },
      });
  }

}
