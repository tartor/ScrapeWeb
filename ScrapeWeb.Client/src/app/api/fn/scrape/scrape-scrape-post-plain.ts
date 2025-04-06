/* tslint:disable */
/* eslint-disable */
/* Code generated by ng-openapi-gen DO NOT EDIT. */

import { HttpClient, HttpContext, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { filter, map } from 'rxjs/operators';
import { StrictHttpResponse } from '../../strict-http-response';
import { RequestBuilder } from '../../request-builder';

import { ScrapeRequest } from '../../models/scrape-request';
import { ScrapeResponse } from '../../models/scrape-response';

export interface ScrapeScrapePost$Plain$Params {
      body?: ScrapeRequest
}

export function scrapeScrapePost$Plain(http: HttpClient, rootUrl: string, params?: ScrapeScrapePost$Plain$Params, context?: HttpContext): Observable<StrictHttpResponse<ScrapeResponse>> {
  const rb = new RequestBuilder(rootUrl, scrapeScrapePost$Plain.PATH, 'post');
  if (params) {
    rb.body(params.body, 'application/*+json');
  }

  return http.request(
    rb.build({ responseType: 'text', accept: 'text/plain', context })
  ).pipe(
    filter((r: any): r is HttpResponse<any> => r instanceof HttpResponse),
    map((r: HttpResponse<any>) => {
      return r as StrictHttpResponse<ScrapeResponse>;
    })
  );
}

scrapeScrapePost$Plain.PATH = '/Scrape/Scrape';
