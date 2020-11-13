import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AtmService {

  private baseUrl: string = null;
  private headers: HttpHeaders = new HttpHeaders().set('Content-Type', 'application/json');

  constructor(private _http: HttpClient) {
    this.baseUrl = environment.apiUrl;
  }


  set(card: string): Observable<any> {
    return this._http.post(`${this.baseUrl}insert`, JSON.stringify(card), { headers: this.headers });
  }

  return(): Observable<any> {
    return this._http.get(`${this.baseUrl}return`);
  }

  balance(): Observable<any> {
    return this._http.get(`${this.baseUrl}balance`);
  }

  withdraw(amount: number): Observable<any> {
    return this._http.post(`${this.baseUrl}withdraw`, JSON.stringify(amount), { headers: this.headers });
  }
}
