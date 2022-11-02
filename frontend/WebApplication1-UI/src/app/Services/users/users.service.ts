import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User } from '../../Models/user.model';

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  baseApIUrl: string = environment.baseApiUrl;

  constructor(private http: HttpClient) { }

      getAllUsers(): Observable<User[]> {
      return this.http.get<User[]>(this.baseApIUrl + '/api/users');
    }
    getUsersNames(): Observable<string[]> {
      return this.http.get<string[]>(this.baseApIUrl + '/api/users/names');
    }
    
      addUser(addUserRequest: User): Observable<User> {
      return this.http.post<User>(this.baseApIUrl + '/api/users', addUserRequest);
    }

      getUser(id: string): Observable<User> {
        return this.http.get<User>(this.baseApIUrl + '/api/users/' + id);
    }

      updateUser(id: string, updateUserRequest: User): Observable<User> {
        return this.http.put<User>(this.baseApIUrl + '/api/users/' + id, updateUserRequest);
    }
      deleteUser(id: string): Observable<User> {
        return this.http.delete<User>(this.baseApIUrl + '/api/users/' + id);
    }
}
