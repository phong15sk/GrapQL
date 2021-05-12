import { Component, OnInit } from '@angular/core';

import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { GraphqlService, IPaginationInput } from '../graphql.service';
import { IOwner, IAccount } from '../types/owners';
import { Subscription } from 'rxjs';
import { QueryRef, gql } from 'apollo-angular';
import { stringify } from '@angular/compiler/src/util';

@Component({
  selector: 'app-owners',
  templateUrl: './owners.component.html',
  styleUrls: ['./owners.component.css'],
})
export class OwnersComponent implements OnInit {
  page = 1;
  pageSize = 2;
  totalRows = 0;
  success = false;
  error = false;
  mess = '';
  closeResult: string | undefined;
  pageInput: IPaginationInput = {
    pageIndex: '1',
    pageSize: '2',
  };
  owner: IOwner = {
    id: '',
    name: '',
    address: '',
    accounts: [],
  };
  isEdit: boolean | undefined;
  titleModal: string | undefined;

  owners: IOwner[] | undefined;

  postsQuery: QueryRef<any> | undefined;
  private querySubscription: Subscription | undefined;

  totalQuery: QueryRef<any> | undefined;
  private querySubscriptionTotal: Subscription | undefined;

  constructor(private modalService: NgbModal, private service: GraphqlService) {
    this.refreshCountries();
  }

  ngOnInit() {
    this.postsQuery = this.service.getOwners(this.pageInput);
    this.querySubscription = this.postsQuery.valueChanges.subscribe(
      ({ data }) => {
        this.owners = data.owners;
      }
    );
    this.totalQuery = this.service.getTotalRowOwner();
    this.querySubscriptionTotal = this.totalQuery.valueChanges.subscribe(
      ({ data }) => {
        this.totalRows = data.getTotalRowOwner.total;
      }
    );
  }

  getTypeAccount(accounts: IAccount[]): string {
    return accounts.reduce((a, b) => {
      return (a += b.type + '; ');
    }, '');
  }

  refresh() {
    this.postsQuery?.refetch();
    this.totalQuery?.refetch();
  }

  ngOnDestroy() {
    this.querySubscription?.unsubscribe();
    this.querySubscriptionTotal?.unsubscribe();
  }

  refreshCountries() {
    this.pageInput.pageIndex = `${this.page}`;
    this.pageInput.pageSize = `${this.pageSize}`;
    this.refresh();
  }

  open(content: any) {
    this.modalService.open(content);
  }

  getAccountsByOwner(owner: IOwner) {
    if (owner.accounts && owner.accounts.length > 0) {
      return owner.accounts;
    }
    return [];
  }

  btnEdit_Click(content: any, owner: IOwner) {
    this.isEdit = true;
    this.titleModal = 'Edit';
    const _accounts = this.getAccountsByOwner(owner);
    this.owner = {
      id: owner.id,
      name: owner.name,
      address: owner.address,
      accounts: _accounts,
    };
    this.open(content);
  }

  btnDelete_Click(ownerId: string) {
    if (window.confirm('Are you sure?')) {
      this.service.deleteOwner(ownerId).subscribe((res) => {
        this.refresh();
        this.mess = 'Delete success';
        this.success = true;
        this.setTimeOutMess();
      }, err => {
        this.mess = 'Delete error';
        this.error = true;
        this.setTimeOutMess();
        console.log(err);
        
      });
    }
  }

  btnAdd_Click(content: any) {
    this.isEdit = false;
    this.titleModal = 'Add';
    this.owner = {
      id: '',
      name: '',
      address: '',
      accounts: [],
    };
    this.open(content);
  }

  btnSave_Click() {
    this.modalService.dismissAll;
    const ownerInput = {
      name: this.owner.name,
      address: this.owner.address,
    };
    this.service.createOwner(ownerInput).subscribe(
      (res) => {
        console.log(res);
        this.refresh();
        this.mess = 'Add new success';
        this.success = true;
        this.setTimeOutMess();
      },
      (err) => {
        this.mess = 'Add new error';
        this.error = true;
        this.setTimeOutMess();
        console.log(err);
        
      }
    );
    this.modalService.dismissAll();
  }

  btnUpdate_Click() {
    this.modalService.dismissAll;
    const ownerInput = {
      name: this.owner.name,
      address: this.owner.address,
    };
    this.service.updateOwner(ownerInput, this.owner.id).subscribe(
      (res) => {
        console.log(res);
        this.refresh();
        this.mess = 'Update success';
        this.success = true;
        this.setTimeOutMess();
      },
      (err) => {
        this.mess = 'Update error';
        this.error = true;
        this.setTimeOutMess();
        console.log(err);
        
      }
    );
    this.modalService.dismissAll();
  }

  setTimeOutMess() {
    setTimeout(() => {
      this.success = false;
      this.error = false;
      this.mess = '';
    }, 3000);
  }
}
