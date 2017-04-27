import React from 'react';

class WriteArticle extends React.Component {
  render() {
    return (
      <div className="writeArticleContainer">
        <h2>
          Your cool title
        </h2>
        <input
          type="text"
          placeholder="Title"
          className="titleInput"
        />
        <h2>
          Description of your article
        </h2>
        <textarea
          type="text"
          placeholder="Description"
          className="descriptionArea"
        />
        <h2>
          Your article
        </h2>
        <textarea
          type="text"
          placeholder="Article"
          className="articleArea"
        />
        <button>
          Publish
        </button>
      </div>
    );
  }
}

module.exports = WriteArticle;
