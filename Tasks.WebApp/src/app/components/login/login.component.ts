import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private authService: AuthService, private router: Router) { }
  data: LoginModel;
  ngOnInit() {
    this.data = { Password: null, UserName: null };
  }

  login(loginFormData: LoginModel, formIsValid) {
    if (formIsValid) {

      this.authService.login(loginFormData).subscribe(res => {

        this.router.navigateByUrl('/tasks');
      }, err => {
        console.log(err);
      });
    }
  }
}
