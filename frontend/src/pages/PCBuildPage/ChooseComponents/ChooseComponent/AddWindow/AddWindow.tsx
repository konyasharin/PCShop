import FiltersPanel from 'components/FiltersPanel/FiltersPanel.tsx';
import React, { ReactNode, useContext, useState } from 'react';
import componentTypes from 'enums/componentTypes.ts';
import useFilters from 'hooks/useFilters.ts';
import styles from './AddWindow.module.css';
import Input from 'components/inputs/Input/Input.tsx';
import ChooseComponentCard from 'components/cards/ChooseComponentCard/ChooseComponentCard.tsx';
import ShowMoreBtn from 'components/btns/ShowMoreBtn/ShowMoreBtn.tsx';
import TProduct from 'types/TProduct.ts';
import config from '../../../../../../config.ts';
import CheckBox from 'components/CheckBox/CheckBox.tsx';
import createClassNames from 'utils/createClassNames.ts';
import productCharacteristics from 'enums/characteristics/productCharacteristics.ts';
import { FiltersContext } from '../../../../../App.tsx';

type AddWindowProps = {
  type: keyof typeof componentTypes;
  searchTitle: string;
  isActive: boolean;
  products: TProduct[] | null;
  onShowMore: () => void;
  setCurrentProduct: (
    newProduct: TProduct | null,
    componentType: keyof typeof componentTypes,
  ) => void;
};

const AddWindow: React.FC<AddWindowProps> = props => {
  const [searchString, setSearchString] = useState('');
  const allFilters = useContext(FiltersContext);
  const { filters, setCheckBoxIsActive } = useFilters(
    'checkBox',
    props.type,
    allFilters,
  );
  const [priceFrom, setPriceFrom] = useState('');
  const [priceTo, setPriceTo] = useState('');
  const checkBoxes: ReactNode[] = [];

  if (filters) {
    const filterKeys: (keyof typeof filters)[] = Object.keys(
      filters ? filters : {},
    ) as (keyof typeof filters)[]; // Нужно для сохранения ссылок на ключи при их передаче в компонент
    filterKeys.forEach(filterKey => {
      const filtersBlocks = filters[filterKey].map((filterBlock, i) => {
        return (
          <CheckBox
            text={filterBlock.text}
            isActive={filterBlock.isActive}
            onChange={() => {
              setCheckBoxIsActive(filterKey, i, !filterBlock.isActive);
            }}
            className={styles.filterBlockElement}
          />
        );
      });
      checkBoxes.push(
        <div>
          <h6
            className={createClassNames([
              styles.filterBlockElement,
              styles.filterBlockElementTitle,
            ])}
          >
            {productCharacteristics[filterKey]}
          </h6>
          {...filtersBlocks}
        </div>,
      );
    });
  }
  const inputs: ReactNode = (
    <div>
      <h6
        className={createClassNames([
          styles.filterBlockElement,
          styles.filterBlockElementTitle,
        ])}
      >
        Цена
      </h6>
      <Input
        value={priceFrom}
        placeholder={'От'}
        onChange={newValue => setPriceFrom(newValue)}
        className={createClassNames([styles.input, styles.filterBlockElement])}
        type={'number'}
      />
      <Input
        value={priceTo}
        placeholder={'До'}
        onChange={newValue => setPriceTo(newValue)}
        className={createClassNames([styles.input, styles.filterBlockElement])}
        type={'number'}
      />
    </div>
  );

  const cards = props.products
    ? props.products.map(product => {
        return (
          <ChooseComponentCard
            name={product.name}
            price={product.price}
            img={`${config.backupUrl}/${product.img}`}
            text={product.description}
            className={styles.card}
            url={`/product/${props.type}/${product.productId}`}
            onClick={() => {
              props.setCurrentProduct(product, props.type);
            }}
          />
        );
      })
    : [];
  return (
    <div className={props.isActive ? styles.window : styles.windowDisable}>
      <FiltersPanel blocks={[...checkBoxes, inputs]} />
      <div className={styles.rightBlock}>
        <h2 className={styles.title}>{props.searchTitle}</h2>
        <Input
          value={searchString}
          onChange={newSearchString => setSearchString(newSearchString)}
          placeholder={'Поиск'}
        />
        {...cards}
        <ShowMoreBtn className={styles.btn} onClick={props.onShowMore}>
          Показать еще...
        </ShowMoreBtn>
      </div>
    </div>
  );
};

export default AddWindow;
