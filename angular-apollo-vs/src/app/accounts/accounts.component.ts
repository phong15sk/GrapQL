import { IOwner } from './../types/owners';
import { Component, OnInit } from '@angular/core';

import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { GraphqlService, IPaginationInput } from '../graphql.service';
import { IAccount } from '../types/owners';
import { Subscription } from 'rxjs';
import { QueryRef } from 'apollo-angular';

@Component({
  selector: 'app-accounts',
  templateUrl: './accounts.component.html',
  styleUrls: ['./accounts.component.css'],
})
export class AccountsComponent implements OnInit {
  page = 1;
  pageSize = 2;
  totalRows = 0;
  loading = false;
  success = false;
  mess = '';
  error = false;
  loadError: any | undefined;
  closeResult: string | undefined;
  pageInput: IPaginationInput = {
    pageIndex: '1',
    pageSize: '2',
  };
  pageInputOwners: IPaginationInput = {
    pageIndex: '1',
    pageSize: '100',
  };
  owners: IOwner[] = [];
  account: IAccount = {
    id: '',
    type: '',
    description: '',
    ownerId: '',
  };
  types = ['CASH', 'SAVINGS', 'EXPENSE', 'INCOME'];
  isEdit: boolean | undefined;
  titleModal: string | undefined;

  accounts: IAccount[] | undefined;

  postsQuery: QueryRef<any> | undefined;
  postsQueryOwner: QueryRef<any> | undefined;
  private querySubscription: Subscription | undefined;
  private querySubscriptionOwner: Subscription | undefined;
  totalAccountQuery: QueryRef<any> | undefined;
  private querySubscriptionTotalAccount: Subscription | undefined;

  constructor(private modalService: NgbModal, private service: GraphqlService) {
    this.refreshCountries();
  }

  ngOnInit() {
    this.postsQueryOwner = this.service.getOwners(this.pageInputOwners);
    this.querySubscriptionOwner = this.postsQueryOwner.valueChanges.subscribe(
      ({ data }) => {
        this.owners = data.owners;
        
      }
    );

    this.postsQuery = this.service.getAccounts(this.pageInput);
    this.querySubscription = this.postsQuery.valueChanges.subscribe(
      (result) => {
        this.accounts = result?.data?.accounts;
        this.loading = result.loading;
        this.loadError = result.error;
      }
    );
    this.totalAccountQuery = this.service.getTotalRowAccount();
    this.querySubscriptionTotalAccount = this.totalAccountQuery.valueChanges.subscribe(
      ({ data }) => {
        this.totalRows = data.getTotalRowAcount.total;
        console.log(data);
        
      }
    );

    this.refresh();
  }

  refresh() {
    this.postsQuery?.refetch();
    this.postsQueryOwner?.refetch();
    this.totalAccountQuery?.refetch();
  }

  ngOnDestroy() {
    this.querySubscription?.unsubscribe();
    this.querySubscriptionOwner?.unsubscribe();
    this.querySubscriptionTotalAccount?.unsubscribe();
  }

  refreshCountries() {
    this.pageInput.pageIndex = `${this.page}`;
    this.pageInput.pageSize = `${this.pageSize}`;
    this.refresh();
  }

  open(content: any) {
    this.modalService.open(content);
  }

  btnEdit_Click(content: any, account: IAccount) {
    console.log(account);
    
    this.isEdit = true;
    this.titleModal = 'Edit';
    this.account = {
      id: account.id,
      type: account.type,
      description: account.description,
      ownerId: account.ownerId,
    };
    this.open(content);
  }

  btnDelete_Click(accountId: string) {
    if (window.confirm('Are you sure?')) {
      this.service.deleteAccount(accountId).subscribe((res) => {
        this.refresh();
        this.mess = 'Delete success';
        this.success = true;
        this.setTimeOutMess();
      },
      err => {
        this.mess = '';
        this.error = true;
        this.setTimeOutMess();
      });
    }
  }

  btnAdd_Click(content: any) {
    this.isEdit = false;
    this.titleModal = 'Add';
    this.account = {
      id: '',
      type: '',
      description: '',
      ownerId: '',
    };
    this.open(content);
  }

  btnSave_Click() {
    this.modalService.dismissAll;
    const accountInput = {
      type: this.account.type,
      description: this.account.description,
      ownerId: this.account.ownerId,
    };
    this.service.createAccount(accountInput).subscribe(
      (res) => {
        this.refresh();
        this.mess = 'Add new success';
        this.success = true;
        this.setTimeOutMess();
        console.log(res.data);
      },
      (err) => {
        this.mess = '';
        this.error = true;
        this.setTimeOutMess();
        console.log(err);
      }
    );
    this.modalService.dismissAll();
  }

  btnUpdate_Click() {
    const accountInput = {
      type: this.account.type,
      description: this.account.description,
      ownerId: this.account.ownerId,
    };
    this.service.updateAccount(accountInput, this.account.id).subscribe(
      (res) => {
        this.refresh();
        this.mess = 'Update success';
        this.success = true;
        this.setTimeOutMess();
        console.log(res.data);
      },
      (err) => {
        this.mess = '';
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
