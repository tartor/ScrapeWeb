/* tslint:disable */
/* eslint-disable */
/* Code generated by ng-openapi-gen DO NOT EDIT. */

import { HttpClient, HttpContext, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { filter, map } from 'rxjs/operators';
import { StrictHttpResponse } from '../../strict-http-response';
import { RequestBuilder } from '../../request-builder';

import { LoginRequest } from '../../models/login-request';
import { LoginResponse } from '../../models/login-response';

export interface LoginLoginPost$Plain$Params {
      body?: LoginRequest
}

export function loginLoginPost$Plain(http: HttpClient, rootUrl: string, params?: LoginLoginPost$Plain$Params, context?: HttpContext): Observable<StrictHttpResponse<LoginResponse>> {
  const rb = new RequestBuilder(rootUrl, loginLoginPost$Plain.PATH, 'post');
  if (params) {
    rb.body(params.body, 'application/*+json');
  }

  return http.request(
    rb.build({ responseType: 'text', accept: 'text/plain', context })
  ).pipe(
    filter((r: any): r is HttpResponse<any> => r instanceof HttpResponse),
    map((r: HttpResponse<any>) => {
      return r as StrictHttpResponse<LoginResponse>;
    })
  );
}

loginLoginPost$Plain.PATH = '/Login/Login';
