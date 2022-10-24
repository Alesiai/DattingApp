import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Photo } from '../_models/photo';
import { User } from '../_models/user';
import { Сomplaints } from '../_models/complaints';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getUsersWithRoles() {
    return this.http.get<Partial<User[]>>(this.baseUrl + 'admin/users-with-roles');
  }

  getСomplaints() {
    return this.http.get<Partial<Сomplaints[]>>(this.baseUrl + 'Сomplaints');
  }

  updateUserRoles(username: string, roles: string[]) {
    return this.http.post(this.baseUrl + 'admin/edit-roles/' + username + '?roles=' + roles, {});
  }

  getPhotosForApproval() {
    return this.http.get<Photo[]>(this.baseUrl + 'admin/photos-to-moderate');
  }

  approvePhoto(photoId: number) {
    return this.http.post(this.baseUrl + 'admin/approve-photo/' + photoId, {});
  }

  rejectPhoto(photoId: number) {
    return this.http.post(this.baseUrl + 'admin/reject-photo/' + photoId, {});
  }

  blockUser(username: string){
    return this.http.post(this.baseUrl + 'users/block-user/'+ username, {})
  }

  unblockUser(username: string){
    return this.http.post(this.baseUrl + 'users/unblock-user/'+ username, {})
  }

  processed(userId: number){
    return this.http.post(this.baseUrl + 'Сomplaints/set-processed/'+ userId, {})
  }

   
}