import {
  ChangeDetectorRef,
  Component,
  OnInit
}
from '@angular/core';

import {
  CommonModule
}
from '@angular/common';

import {
  FormsModule
}
from '@angular/forms';

import {
  MatTableModule
}
from '@angular/material/table';

import {
  MatButtonModule
}
from '@angular/material/button';

import {
  MatInputModule
}
from '@angular/material/input';

import {
  MatFormFieldModule
}
from '@angular/material/form-field';

import {
  SponsorshipTypeService
}
from '../../services/sponsorship-type';


@Component({
  selector: 'app-sponsorship-type-list',
  imports: [    CommonModule,
    FormsModule,
    MatTableModule,
    MatButtonModule,
    MatInputModule,
    MatFormFieldModule],
  templateUrl: './sponsorship-type-list.html',
  styleUrl: './sponsorship-type-list.scss',
})
export class SponsorshipTypeList implements OnInit {

  types: any[] = [];

  newName = '';

  displayedColumns = [
    'name',
    'actions'
  ];

  constructor(
    private typeService:
      SponsorshipTypeService,
      private cdr: ChangeDetectorRef
  ) {

  }

  ngOnInit(): void {

    this.load();
  }

  load(): void {

    this.typeService
      .getAll()
      .subscribe(response => {

        this.types =
          (response as any).data ??
          response;
        this.cdr.detectChanges();
      });
  }

  add(): void {

    if (!this.newName) {
      return;
    }

    this.typeService
      .create(this.newName)
      .subscribe(() => {

        this.newName = '';

        this.load();
      });
  }

  update(item: any): void {

    this.typeService
      .update(
        item.id,
        item.name
      )
      .subscribe(() => {

        this.load();
      });
  }

  delete(id: string): void {

    this.typeService
      .delete(id)
      .subscribe(() => {

        this.load();
      });
  }
}
