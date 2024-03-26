import React from 'react';
import TCheckBox from 'types/TCheckBox.ts';
import Radio from 'components/Radio/Radio.tsx';
import componentTypes from 'enums/componentTypes.ts';
import styles from '../EditProduct/EditProduct.module.css';

type CategoriesBlocksProps = {
  categories: TCheckBox[];
  setCategoryIsActive: (index: number) => void;
  setActiveCategory: (type: keyof typeof componentTypes) => void;
  className?: string;
};

const CategoriesBlocks: React.FC<CategoriesBlocksProps> = props => {
  const categoriesBlocks = props.categories.map((category, i) => {
    return (
      <Radio
        text={category.text}
        isActive={category.isActive}
        onChange={() => {
          props.setCategoryIsActive(i);
          switch (category.text) {
            case 'Видеокарты':
              props.setActiveCategory('videoCard');
              break;
            case 'Процессоры':
              props.setActiveCategory('processor');
              break;
          }
        }}
        className={styles.checkBox}
      />
    );
  });
  return (
    <div className={props.className ? props.className : ''}>
      {...categoriesBlocks}
    </div>
  );
};

export default CategoriesBlocks;
