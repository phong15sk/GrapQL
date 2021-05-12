import { Injectable } from '@angular/core';

import { Apollo, gql } from 'apollo-angular';
import { IAccount, IAccountInput, IOwner, IOwnerInput } from './types/owners';

const GET_OWNER = gql`
  query getOwner($ownerID: ID!) {
    owner(ownerId: $ownerID) {
      id
      name
      address
      accounts {
        id
        description
        type
      }
    }
  }
`;

const GET_OWNERS = gql`
  query getOwners($page: paginationInput!) {
    owners(pagination: $page) {
      id
      name
      address
      accounts {
        id
        description
        type
      }
    }
  }
`;

const CREATE_OWNER = gql`
  mutation ($owner: ownerInput!) {
    createOwner(owner: $owner) {
      id
      name
      address
    }
  }
`;

const UPDATE_OWNER = gql`
  mutation ($owner: ownerInput!, $ownerId: ID!) {
    updateOwner(owner: $owner, ownerId: $ownerId) {
      id
      name
      address
    }
  }
`;

const DELETE_OWNER = gql`
  mutation ($ownerId: ID!) {
    deleteOwner(ownerId: $ownerId)
  }
`;

// =======================
const GET_ACCOUNT = gql`
  query getAccount($accountID: ID!) {
    account(accountId: $accountID) {
      id
      name
      address
      accounts {
        id
        description
        type
      }
    }
  }
`;

const GET_ACCOUNTS = gql`
  query getAccounts($page: paginationInput!) {
    accounts(pagination: $page) {
      id
      description
      type
      ownerId
    }
  }
`;

const CREATE_ACCOUNT = gql`
  mutation ($account: accountInput!) {
    createAccount(account: $account) {
      type
      description
      ownerId
    }
  }
`;

const UPDATE_ACCOUNT = gql`
  mutation ($account: accountInput!, $accountID: ID!) {
    updateAccount(account: $account, accountId: $accountID) {
      type
      description
      ownerId
    }
  }
`;

const DELETE_ACCOUNT = gql`
  mutation ($accountID: ID!) {
    deleteAccount(accountId: $accountID)
  }
`;
// ==
const GET_TOTAL_ROW_OWNER = gql`
  query {
    getTotalRowOwner {
      total
    }
  }
`;
const GET_TOTAL_ROW_ACCOUNT = gql`
  query {
    getTotalRowAcount {
      total
    }
  }
`;

export interface IPaginationInput {
  pageIndex: string;
  pageSize: string;
}

@Injectable({
  providedIn: 'root',
})
export class GraphqlService {
  constructor(private apollo: Apollo) {}

  getOwners(page: IPaginationInput) {
    return this.apollo.watchQuery<IOwner[]>({
      query: GET_OWNERS,
      variables: {
        page: page,
      },
    });
  }

  getOwner(ownerID: string) {
    return this.apollo.query({
      query: GET_OWNER,
      variables: {
        ownerID,
      },
    });
  }

  createOwner(ownerToUpdate: IOwnerInput) {
    return this.apollo.mutate({
      mutation: CREATE_OWNER,
      variables: { owner: ownerToUpdate },
    });
  }

  updateOwner(ownerToUpdate: IOwnerInput, id: string) {
    return this.apollo.mutate({
      mutation: UPDATE_OWNER,
      variables: { owner: ownerToUpdate, ownerId: id },
    });
  }

  deleteOwner(id: string) {
    return this.apollo.mutate({
      mutation: DELETE_OWNER,
      variables: { ownerId: id },
    });
  }
  // =========================
  getAccounts(page: IPaginationInput) {
    return this.apollo.watchQuery<IAccount[]>({
      query: GET_ACCOUNTS,
      variables: {
        page: page,
      },
    });
  }

  getAccount(accountID: string) {
    return this.apollo.query({
      query: GET_ACCOUNT,
      variables: {
        accountID,
      },
    });
  }

  createAccount(account: IAccountInput) {
    return this.apollo.mutate({
      mutation: CREATE_ACCOUNT,
      variables: { account: account },
    });
  }

  updateAccount(accountToUpdate: IAccountInput, id: string) {
    return this.apollo.mutate({
      mutation: UPDATE_ACCOUNT,
      variables: { account: accountToUpdate, accountID: id },
    });
  }

  deleteAccount(id: string) {
    return this.apollo.mutate({
      mutation: DELETE_ACCOUNT,
      variables: { accountID: id },
    });
  }

  getTotalRowOwner() {
    return this.apollo.watchQuery<number>({
      query: GET_TOTAL_ROW_OWNER,
    });
  }
  getTotalRowAccount() {
    return this.apollo.watchQuery<number>({
      query: GET_TOTAL_ROW_ACCOUNT,
    });
  }
}
