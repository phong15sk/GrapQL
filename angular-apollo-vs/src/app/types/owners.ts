export interface IOwner {
  id: string;
  name: string;
  address: string;
  accounts?: IAccount[];
}

export interface IAccount {
  id: string;
  description: string;
  ownerId: string;
  type: string;
}

export interface IOwnerInput {
  name: string;
  address: string;
}

export interface IAccountInput {
  type: string;
  description: string;
  ownerId: string;
}
