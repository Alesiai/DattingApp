import { Component, OnInit } from '@angular/core';
import { Сomplaints } from 'src/app/_models/complaints'
import { AdminService } from 'src/app/_services/admin.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-user-block-managment',
  templateUrl: './user-block-managment.component.html',
  styleUrls: ['./user-block-managment.component.css']
})
export class UserBlockManagmentComponent implements OnInit {
  complaints: Partial<Сomplaints[]>

  constructor(private accountService: AdminService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.getAllInfoUsers();
  }

  getAllInfoUsers() {
    this.accountService.getСomplaints().subscribe(complaints => {
      this.complaints = complaints;
    })
  }

  approve(complaint: Сomplaints){
    this.accountService.blockUser(complaint.userName).subscribe();
    
      this.accountService.processed(complaint.id).subscribe(()=> {
        this.complaints.splice(this.complaints.findIndex(p => p.id === complaint.id), 1)});
    
    
    this.toastr.success('the application is accepted, the user is blocked');
    this.getAllInfoUsers();
  }
  reject(id: number){
    this.accountService.processed(id).subscribe(()=> {
      this.complaints.splice(this.complaints.findIndex(p => p.id === id), 1)});
  
    this.toastr.success('application rejected');
    this.getAllInfoUsers();
  }
}
