import { Injectable } from '@angular/core';
import {
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
  HttpResponse,
} from '@angular/common/http';
import { Observable } from 'rxjs';

import { LoaderService } from './loader.service';

@Injectable({
  providedIn: 'root',
})
export class Interceptor implements HttpInterceptor {
  constructor(
    //private _global: GlobalService,
   private loadingService: LoaderService
    //private _fuseConfirmationService: FuseConfirmationService
  ) {}
  public countRequest: HttpRequest<any>[] = [];

  removeRequest(req: HttpRequest<any>) {
    const i = this.countRequest.indexOf(req);
    if (i >= 0) {
      this.countRequest.splice(i, 1);
    }
    if (this.countRequest.length == 0) {
      //this._loading.showLoading(false);
    }
  }

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    this.countRequest.push(req);
     
    if (this.countRequest.length > 0) {
      this.loadingService.setLoading(true);
    }
    
      req = req.clone({
        headers: req.headers.set(
          'apikey',
          '08OInHqSoeux2HPGs0bukF4j6nQIU7AX'
        ),
      });
    

    if (!req.headers.has('Content-Type')) {
      req = req.clone({
        headers: req.headers.set('Content-Type', 'application/json'),
      });
    }

    return Observable.create((observer:any) => {
      let mensajeError = {};
      const subscription = next.handle(req).subscribe(
        (event) => {
          if (event instanceof HttpResponse) {
            this.removeRequest(req);
            observer.next(event);
          }
        },
        (err) => {
          this.removeRequest(req);
          observer.error(err);
          if (err.status === 401) {
          } else if (err.status === 400) {
          } else if (err.status === 500) {
          } else {
          }
        },
        () => {
          this.removeRequest(req);
          observer.complete();
        }
      );
      return () => {
        this.removeRequest(req);
        subscription.unsubscribe();
        this.loadingService.setLoading(false);
      };
    });
  }
}
