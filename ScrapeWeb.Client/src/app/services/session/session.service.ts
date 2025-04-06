import { Injectable, inject } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { ApiService } from '../../api/api.service';
import { sessionValidatePost } from '../../api/fn/session/session-validate-post';
import { SessionResponse } from '../../api/models/session-response';


@Injectable({
  providedIn: 'root'
})
export class SessionService {

  private readonly _apiService = inject(ApiService);

  private _loggedIn: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  private _validate: Observable<SessionResponse> = this._apiService.invoke(sessionValidatePost);

  constructor() { }
  
  public get loggedIn$(): BehaviorSubject<boolean> {
    return this._loggedIn;
  }

  public get validate$(): Observable<SessionResponse> {
    return this._validate;
  }

}
