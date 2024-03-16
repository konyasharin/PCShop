import FiltersPanel from 'components/FiltersPanel/FiltersPanel.tsx';
import React, { ReactNode, useState } from 'react';
import EComponentTypes from 'enums/EComponentTypes.ts';
import useFilters from 'hooks/useFilters.ts';
import { useSelector } from 'react-redux';
import { RootState } from 'store/store.ts';
import styles from './AddWindow.module.css';
import Input from 'components/Input/Input.tsx';
import ChooseComponentCard from 'components/cards/ChooseComponentCard/ChooseComponentCard.tsx';
import ShowMoreBtn from 'components/btns/ShowMoreBtn/ShowMoreBtn.tsx';
import TProduct from 'types/TProduct.ts';
import config from '../../../../../../config.ts';
import CheckBox from 'components/CheckBox/CheckBox.tsx';
import Radio from 'components/Radio/Radio.tsx';
import createClassNames from 'utils/createClassNames.ts';

type AddWindowProps = {
  type: EComponentTypes;
  searchTitle: string;
  isActive: boolean;
  products: TProduct[];
  onShowMore: () => void;
};

const AddWindow: React.FC<AddWindowProps> = props => {
  const [searchString, setSearchString] = useState('');
  const { filters, setCheckBoxIsActive, setRadioIsActive } = useFilters(
    useSelector((state: RootState) => state.filters[props.type]),
  );
  const [priceFrom, setPriceFrom] = useState('');
  const [priceTo, setPriceTo] = useState('');
  const checkBoxes: ReactNode[] = [];
  filters.forEach(filter => {
    if (filter.type === 'checkBox') {
      const filtersBlocks = filter.filters.map((filterBlock, i) => {
        return (
          <CheckBox
            text={filterBlock.text}
            isActive={filterBlock.isActive}
            onChange={() => {
              setCheckBoxIsActive(filter.name, i, !filterBlock.isActive);
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
            {filter.name}
          </h6>
          {...filtersBlocks}
        </div>,
      );
    }
  });

  const radios: ReactNode[] = [];
  filters.forEach(filter => {
    if (filter.type === 'radio') {
      const radiosBlocks = filter.filters.map((filterBlock, i) => {
        return (
          <Radio
            text={filterBlock.text}
            isActive={filterBlock.isActive}
            onChange={() => {
              setRadioIsActive(filter.name, i);
            }}
            className={styles.filterBlockElement}
          />
        );
      });
      radios.push(
        <div>
          <h6
            className={createClassNames([
              styles.filterBlockElement,
              styles.filterBlockElementTitle,
            ])}
          >
            {filter.name}
          </h6>
          {...radiosBlocks}
        </div>,
      );
    }
  });
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
  // const inputsBlocks = filters.inputsBlocks.map(inputsBlock => {
  //   return (
  //     <FilterBlock title={inputsBlock.name}>
  //       <InputsBlock
  //         inputs={inputsBlock.inputs}
  //         onChange={(name, newValue) => setInputValue(name, newValue)}
  //         inputClassName={styles.filterBlockElement}
  //       />
  //     </FilterBlock>
  //   );

  const cards = props.products.map(product => {
    return (
      <ChooseComponentCard
        name={product.name}
        price={product.price}
        img={`${config.backupUrl}/${product.img}`}
        text={product.description}
        className={styles.card}
      />
    );
  });
  return (
    <div className={props.isActive ? styles.window : styles.windowDisable}>
      <FiltersPanel
        type={props.type}
        blocks={[...checkBoxes, ...radios, inputs]}
      />
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
