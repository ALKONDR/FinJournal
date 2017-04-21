import { observable } from 'mobx';

class HeaderState {
  @observable opened = false;
}

module.exports = new HeaderState();
