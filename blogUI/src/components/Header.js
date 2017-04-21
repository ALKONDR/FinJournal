import React from 'react';
import NameAndLogo from './NameAndLogo';
import Search from './Search';
import HeaderState from './HeaderState';

class Header extends React.Component {
  render() {
    return (
      <div className="header">
        <NameAndLogo />
        <Search headerState={HeaderState} />
      </div>
    );
  }
}

module.exports = Header;
