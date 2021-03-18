import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { NgForm } from '@angular/forms';
import { StudentDTO } from '../models/students.model';
import { config } from '../Shared/config';

@Injectable({
  providedIn: 'root'
})
export class StudentsService {
  constructor(private http: HttpClient) { }

  addStudent(formData: StudentDTO) {
    return this.http.post(config.EndPoint + config.Students.AddStudentUrl, formData);
  }
}
