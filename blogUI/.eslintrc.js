module.exports = {
  parser: "babel-eslint",
  "rules": {
    "import/no-extraneous-dependencies": [
      "error",
      {
        "devDependencies": true
      }
    ]
  },
  "extends": "airbnb",
  "plugins": [
    "react",
    "jsx-a11y",
    "import"
  ],
  "globals": {
    "document": true
  }
};