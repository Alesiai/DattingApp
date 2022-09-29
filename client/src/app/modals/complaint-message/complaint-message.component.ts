import { Component, OnInit, Input, EventEmitter } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { MemberDetailComponent } from 'src/app/members/member-detail/member-detail.component';
import { Сomplaint } from 'src/app/_models/complaint';
import { AccountService } from 'src/app/_services/account.service';
import { AdminService } from 'src/app/_services/admin.service';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-complaint-message',
  templateUrl: './complaint-message.component.html',
  styleUrls: ['./complaint-message.component.css']
})
export class ComplaintMessageComponent implements OnInit {

  @Input() updateSelectedRoles = new EventEmitter();

  constructor(
    public bsModalRef: BsModalRef, 
    private memberService: MembersService, private fb: FormBuilder, private adminService: AdminService,
    private toastr: ToastrService ) { }

  userName: string = "";
  complaintForm: FormGroup;
  message: string ="";

  ngOnInit(): void {
    this.userName = this.memberService.getUsername();
    this.intitializeForm();
  }

  intitializeForm() {
    this.complaintForm = this.fb.group({
      message: ['', Validators.required],
      userName: [this.userName],
    })
  }

  sendComplaint() : void{
    let complaint:Сomplaint = this.complaintForm.value;
    this.memberService.createComplaint(complaint).subscribe(() => {
      this.toastr.success('your application has been submitted for review');
    });
  }
}
