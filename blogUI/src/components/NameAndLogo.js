import React from 'react';

class NameAndLogo extends React.Component {
  render() {
    return (
      <div className="nameAndLogo" >
        <img alt="logo" className="logo" />
      </div>
    );
  }
}

// <h1 className="name">
//           FinJournal
//         </h1>

module.exports = NameAndLogo;
