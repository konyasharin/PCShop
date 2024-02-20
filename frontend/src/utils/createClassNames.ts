function createClassNames(classNamesList: Array<string | undefined>) {
  let classNames = '';
  classNamesList.forEach((className, i) => {
    if (className) {
      if (i !== 0) {
        classNames += ` ${className}`;
      } else {
        classNames += className;
      }
    }
  });
  return classNames;
}

export default createClassNames;
