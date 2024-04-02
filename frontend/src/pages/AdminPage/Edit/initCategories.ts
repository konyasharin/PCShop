import componentTypes from 'enums/componentTypes.ts';

function initCategories() {
  let key: keyof typeof componentTypes;
  const categories = [];
  for (key in componentTypes) {
    categories.push({
      text: componentTypes[key],
      isActive: false,
    });
  }
  categories[0].isActive = true;
  return categories;
}

export default initCategories;
