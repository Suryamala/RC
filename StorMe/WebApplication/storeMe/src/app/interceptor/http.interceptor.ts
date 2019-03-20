
import { Injectable } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpResponse, HttpErrorResponse } from '@angular/common/http';
import { Router, ActivatedRoute } from '@angular/router';

import { Observable, throwError } from 'rxjs';
import { map, catchError } from 'rxjs/Operators';

import { environment } from '../../environments/environment';


@Injectable()
export class InterceptedHttp implements HttpInterceptor {
    constructor(private _router: Router) { }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        var timestamp = new Date().getTime();
        const _req = req.clone({ url: environment.api_url + req.url  });
        if (req.url.indexOf("login") == -1) {

            // Clone the request and set the new header in one step.
            //const authReq = _req.clone({ setHeaders: { 'Authorization': `Bearer ${authToken}` } });

            // send cloned request with header to the next handler.
            return next.handle(_req)
            .pipe(
                map((event: HttpEvent<any>) => {
                    if (event instanceof HttpResponse) {

                    }
                    return event;
                }),
                catchError((error: HttpErrorResponse) => {
                    let data = {};
                    data = {
                        reason: error && error.error&& error.error.reason ? error.error.reason : '',
                        status: error.status
                    };
                    return throwError(error);
                }));
        }
        else {
            const req = _req.clone({ setHeaders: { "Content-Type" : "application/x-www-form-urlencoded" } });
            return next.handle(req);
        }
    }
}
