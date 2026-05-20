import { Injectable } from '@angular/core';

import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { environment }
from '../../../../environments/environment';

import { SponsorshipType }
from '../../../core/models/sponsorship-type.model';

@Injectable({
  providedIn: 'root',
})
export class SponsorshipTypeService  {
   private apiUrl =
    `${environment.apiUrl}/SponsorshipTypes`;

  constructor(
    private http: HttpClient
  ) {

  }

  getAll():
    Observable<SponsorshipType[]> {

    return this.http.get<SponsorshipType[]>(
      this.apiUrl
    );
  }

  create(name: string) {

    return this.http.post(
      this.apiUrl,
      { name }
    );
  }

  update(id: string, name: string) {

    return this.http.put(
      `${this.apiUrl}/${id}`,
      { name }
    );
  }

  delete(id: string) {

    return this.http.delete(
      `${this.apiUrl}/${id}`
    );
  }
}
