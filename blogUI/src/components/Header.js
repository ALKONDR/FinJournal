import { observable } from 'mobx';
import React from 'react';
import NameAndLogo from './NameAndLogo';
import Search from './Search';

class HeaderState {
  @observable opened = false;
}

class Header extends React.Component {
  render() {
    return (
      <div className="header">
        <NameAndLogo />
        <Search headerState={new HeaderState()} />
      </div>
    );
  }
}

module.exports = Header;
