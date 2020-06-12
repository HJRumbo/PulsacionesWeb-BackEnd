import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HandleHttpErrorService } from '../@base/handle-http-error.service';
import { Observable } from 'rxjs';
import { User } from '../seguridad/user';
import { tap, catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  baseUrl: string;

constructor(

private http: HttpClient,
@Inject('BASE_URL') baseUrl: string,
private handleErrorService: HandleHttpErrorService)
{
this.baseUrl = baseUrl;
}

  post(user: User): Observable<User> {


    return this.http.post<User>(this.baseUrl + 'api/User', user)
      .pipe(

        tap(_ => this.handleErrorService.log('datos enviados')),
    
        catchError(this.handleErrorService.handleError<User>('Registrar User', null))

  );

}
}
