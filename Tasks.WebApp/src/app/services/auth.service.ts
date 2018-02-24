import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/of';

@Injectable()
export class AuthService {

  constructor(private httpClient: HttpClient) { }
  currentUser : UserInfoModel;
  login(data: LoginModel): Observable<any> {
    this.currentUser = null;
    return this.httpClient.post('/auth/login', data);
  }

  getProfile(): Observable<UserInfoModel> {
    if(this.currentUser== null){
      return this.httpClient.get<UserInfoModel>('/auth/getmyinfo').map(res=>this.currentUser = res);
    }
      else{
        return Observable.of(this.currentUser);
      }
      
}

}
