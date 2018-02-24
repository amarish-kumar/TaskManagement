import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Route, Router, CanLoad } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { AuthService } from './services/auth.service';
import 'rxjs/add/operator/map';


@Injectable()
export class AppGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router) { }
  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {

        return this.authService.getProfile().map(res => {
            if (res!= null) {
                return true;
          }
          return false;
          }, err => {
            console.log(err);
            return false;
          });
  }
}