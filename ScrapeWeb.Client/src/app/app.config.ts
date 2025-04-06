import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { provideRouter } from '@angular/router';
import { ApiConfiguration } from './api/api-configuration';

import { routes } from './app.routes';

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideHttpClient(
      withInterceptors([
        (request, next) => {
          const copiedReq = request.clone({
            withCredentials: true
          });
          return next(copiedReq);
        }
      ]),
     ),
    {
      provide: ApiConfiguration,
      useFactory: () => {
        const apiConfig = new ApiConfiguration();
        apiConfig.rootUrl = "http://localhost:5293";
        //apiConfig.rootUrl = "https://localhost:7031";
        return apiConfig;
      },
    }      
  ]
};
