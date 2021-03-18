import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { config } from '../Shared/config';
import { StudentDTO } from '../models/students.model';
import { LookupsDTO } from '../models/lookups.model';

@Injectable({
  providedIn: 'root'
})
export class QueryService {
  constructor(private httpClient: HttpClient) { }

  getAllGrades(): Observable<LookupsDTO[]> {
    return this.httpClient.get<LookupsDTO[]>(config.EndPoint + config.Queries.GetAllGradesUrl);
  }

  getAllClasses(): Observable<LookupsDTO[]> {
    return this.httpClient.get<LookupsDTO[]>(config.EndPoint + config.Queries.GetAllClassesUrl);
  }

  getAllStudents(): Observable<StudentDTO[]> {
    return this.httpClient.get<StudentDTO[]>(config.EndPoint + config.Queries.GetAllStudentsUrl);
  }

  getStudentById(id: number): Observable<StudentDTO[]> {
    return this.httpClient.get<StudentDTO[]>(config.EndPoint + config.Queries.GetStudentsByIdUrl + `?studentId=${id}`);
  }

  getClassesByGradeId(id: number): Observable<LookupsDTO[]> {
    return this.httpClient.get<LookupsDTO[]>(config.EndPoint + config.Queries.GetClassesByGradeIdUrl + `?gradeId=${id}`);
  }
}
