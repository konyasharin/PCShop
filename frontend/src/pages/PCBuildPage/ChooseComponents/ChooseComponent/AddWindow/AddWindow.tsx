import FiltersPanel from 'components/FiltersPanel/FiltersPanel.tsx';
import React, { ReactNode, useState } from 'react';
import EComponentTypes from 'enums/EComponentTypes.ts';
import useFilters from 'hooks/useFilters.ts';
import { useSelector } from 'react-redux';
import { RootState } from 'store/store.ts';
import CheckBoxesBlock from 'components/CheckBoxexBlock/CheckBoxesBlock.tsx';
import RadiosBlock from 'components/RadiosBlock/RadiosBlock.tsx';
import FilterBlock from 'components/FiltersPanel/FilterBlock/FilterBlock.tsx';
import styles from './AddWindow.module.css';
import Input from 'components/Input/Input.tsx';
import ChooseComponentCard from 'components/cards/ChooseComponentCard/ChooseComponentCard.tsx';
import ShowMoreBtn from 'components/btns/ShowMoreBtn/ShowMoreBtn.tsx';
import TProduct from 'types/TProduct.ts';
import config from '../../../../../../config.ts';

type AddWindowProps = {
  type: EComponentTypes;
  searchTitle: string;
  isActive: boolean;
  products: TProduct[];
};

const AddWindow: React.FC<AddWindowProps> = props => {
  const [searchString, setSearchString] = useState('');
  const { filters, setCheckBoxIsActive, setRadioIsActive } = useFilters(
    // Потом нужно добавить в редакс все типы компонентов
    useSelector((state: RootState) => state.filters[props.type]),
  );
  const checkBoxes: ReactNode[] = [];
  filters.forEach(filter => {
    if (filter.type === 'checkBox') {
      checkBoxes.push(
        <FilterBlock title={filter.name}>
          <CheckBoxesBlock
            title={filter.name}
            checkBoxes={filter.filters}
            checkBoxClassName={styles.filterBlockElement}
            onChange={(nameBlock, index, newIsActive) => {
              setCheckBoxIsActive(nameBlock, index, newIsActive);
            }}
          />
        </FilterBlock>,
      );
    }
  });

  const radiosBlocks: ReactNode[] = [];
  filters.forEach(filter => {
    if (filter.type === 'radio') {
      radiosBlocks.push(
        <FilterBlock title={filter.name}>
          <RadiosBlock
            title={filter.name}
            radios={filter.filters}
            onChange={(nameBlock, index) => {
              setRadioIsActive(nameBlock, index);
            }}
            radioClassName={styles.filterBlockElement}
          />
        </FilterBlock>,
      );
    }
  });

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
        checkBoxesBlocks={checkBoxes}
        radiosBlocks={radiosBlocks}
        inputsBlocks={[]}
      />
      <div className={styles.rightBlock}>
        <h2 className={styles.title}>{props.searchTitle}</h2>
        <Input
          value={searchString}
          onChange={newSearchString => setSearchString(newSearchString)}
          placeholder={'Поиск'}
        />
        {...cards}
        <ShowMoreBtn className={styles.btn}>Показать еще...</ShowMoreBtn>
      </div>
    </div>
  );
};

export default AddWindow;
