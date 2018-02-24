import { Component, OnInit } from '@angular/core';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent  implements OnInit {
 currentUser : UserInfoModel;
  constructor(private authService:AuthService) {
  }
  ngOnInit(): void {
    this.authService.getProfile().subscribe(res=>{
      this.currentUser = res;
    });
  }
  title = 'Tasks Management System';
}
