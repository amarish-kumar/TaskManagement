import { Injectable } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { environment } from '../environments/environment';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/catch';
@Injectable()
export class AppHttpInterceptor implements HttpInterceptor {
  constructor() {
  }
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    const headers = req.headers;
    if (!(req.body instanceof FormData)) {
      headers.append('Content-Type', 'application/json');
      headers.append('Cache-control', 'no-cache');
    }
    const options = { headers: headers, withCredentials: true, url: environment.ServiceUrl + req.url };
    const authReq = req.clone(options);

    return next.handle(authReq).do(event => {

    }).catch((err: any, caught) => {
      if (err instanceof HttpErrorResponse) {
        return Observable.throw(err);
      }
    });
  }
}
