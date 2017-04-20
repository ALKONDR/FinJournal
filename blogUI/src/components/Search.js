import React from 'react';

class Search extends React.Component {
  render() {
    return (
      <div className="search">
        <input
          type="text"
          className="searchInput"
          placeholder="search"
        />
        <img
          className="searchIcon"
          alt="searh"
        />
      </div>
    );
  }
}

module.exports = Search;
