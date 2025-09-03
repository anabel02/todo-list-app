import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../../environment';
import {Observable} from 'rxjs';
import {ODataService} from './odata-base.service';
import {Profile} from '../models/profile.model';

@Injectable({providedIn: 'root'})
export class ProfileService extends ODataService<Profile> {
  protected override baseUrl = `${environment.apiBaseUrl}/profiles`;

  constructor(http: HttpClient) {
    super(http);
  }

  create(): Observable<Profile> {
    return this.http.post<Profile>(this.baseUrl, {});
  }
}
