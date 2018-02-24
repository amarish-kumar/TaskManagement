import { Component, OnInit } from '@angular/core';
import { TaskService } from '../../services/task.services';
import { Route, Router, ActivatedRoute } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-task-form',
  templateUrl: './task-form.component.html',
  styleUrls: ['./task-form.component.css']
})
export class TaskFormComponent implements OnInit {

  currentUser: UserInfoModel;
  data: CreateTaskModel;
  taskId: string;
  users: {};
  allowAssign: boolean;
  constructor(private authService: AuthService, private taskService: TaskService, private router: Router, private route: ActivatedRoute) {
    this.taskId = this.route.snapshot.params['taskId'];
  }



  ngOnInit() {
    this.data = { Id: '', Desc: '', Title: '', DueDate: null, AssignedTo: '-1', CreatedBy: '', Status: '' };

    this.taskService.GetUserList().subscribe(res => {

      this.users = res;

      if (this.taskId != null) {
        this.authService.getProfile().subscribe(res => {
          this.currentUser = res;
        });
        this.taskService.GetTaskDetails(this.taskId).subscribe(res => {

          this.data = res;
          this.allowAssign = (this.data.AssignedTo == this.currentUser.UserId);
        }, err => {
          console.log(err);
        });
      }
    }, err => {
      console.log(err);
    });







  }
  submit(data, isvalid) {
    if (isvalid) {

      this.taskService.CreateTask(this.data).subscribe(res => {

        this.router.navigateByUrl('/tasks');
      }, err => {
        console.log(err);
      });

    }
  }

  OnClickUpdate() {
    if (this.taskId != null) {
      this.taskService.UpdateTask(this.data).subscribe(res => {

        this.router.navigateByUrl('/tasks');
      }, err => {
        console.log(err);
      });

    }
  }

  OnClickAssign() {
    if (this.taskId != null) {
      this.taskService.AssignTask(this.data).subscribe(res => {

        this.router.navigateByUrl('/tasks');
      }, err => {
        console.log(err);
      });

    }
  }
}
