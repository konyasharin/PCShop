import FiltersPanel from 'components/FiltersPanel/FiltersPanel.tsx';
import React, { useState } from 'react';
import EComponentTypes from 'enums/EComponentTypes.ts';
import useFilters from 'hooks/useFilters.ts';
import { useSelector } from 'react-redux';
import { RootState } from 'store/store.ts';
import CheckBoxesBlock from 'components/CheckBoxexBlock/CheckBoxesBlock.tsx';
import RadiosBlock from 'components/RadiosBlock/RadiosBlock.tsx';
import InputsBlock from 'components/FiltersPanel/InputsBlock/InputsBlock.tsx';
import FilterBlock from 'components/FiltersPanel/FilterBlock/FilterBlock.tsx';
import styles from './AddWindow.module.css';
import Input from 'components/Input/Input.tsx';
import ChooseComponentCard from 'components/cards/ChooseComponentCard/ChooseComponentCard.tsx';
import videoCardImg from 'assets/videocard.jpg';
import Btn from 'components/Btn/Btn.tsx';

type AddWindowProps = {
  type: EComponentTypes;
  searchTitle: string;
  isActive: boolean;
};

const AddWindow: React.FC<AddWindowProps> = props => {
  const [searchString, setSearchString] = useState('');
  const { filters, setCheckBoxIsActive, setRadioIsActive, setInputValue } =
    useFilters(
      // Потом нужно добавить в редакс все типы компонентов
      // @ts-expect-ignore
      useSelector((state: RootState) => state.filters[props.type]),
    );
  const checkBoxes = filters.checkBoxesBlocks.map(checkBoxBlock => {
    return (
      <FilterBlock title={checkBoxBlock.name}>
        <CheckBoxesBlock
          title={checkBoxBlock.name}
          checkBoxes={checkBoxBlock.checkBoxes}
          onChange={(nameBlock, index, newIsActive) =>
            setCheckBoxIsActive(nameBlock, index, newIsActive)
          }
          checkBoxClassName={styles.filterBlockElement}
        />
      </FilterBlock>
    );
  });

  const radiosBlocks = filters.radiosBlocks.map(radioBlock => {
    return (
      <FilterBlock title={radioBlock.name}>
        <RadiosBlock
          title={radioBlock.name}
          radios={radioBlock.checkBoxes}
          onChange={(nameBlock, index) => {
            setRadioIsActive(nameBlock, index);
          }}
          radioClassName={styles.filterBlockElement}
        />
      </FilterBlock>
    );
  });

  const inputsBlocks = filters.inputsBlocks.map(inputsBlock => {
    return (
      <FilterBlock title={inputsBlock.name}>
        <InputsBlock
          inputs={inputsBlock.inputs}
          onChange={(name, newValue) => setInputValue(name, newValue)}
          inputClassName={styles.filterBlockElement}
        />
      </FilterBlock>
    );
  });
  return (
    <div className={props.isActive ? styles.window : styles.windowDisable}>
      <FiltersPanel
        type={props.type}
        checkBoxesBlocks={checkBoxes}
        radiosBlocks={radiosBlocks}
        inputsBlocks={inputsBlocks}
      />
      <div className={styles.rightBlock}>
        <h2>{props.searchTitle}</h2>
        <Input
          value={searchString}
          onChange={newSearchString => setSearchString(newSearchString)}
          placeholder={'Поиск'}
        />
        <ChooseComponentCard
          name={'Gigabyte RTX 3080'}
          text={
            '8GB, здесь типо какие-то характеристики здесь типо какие-то характеристики, здесь типо какие-то харак'
          }
          price={100}
          img={videoCardImg}
          className={styles.card}
        />
        <ChooseComponentCard
          name={'Gigabyte RTX 3080'}
          text={
            '8GB, здесь типо какие-то характеристики здесь типо какие-то характеристики, здесь типо какие-то харак'
          }
          price={100}
          img={videoCardImg}
          className={styles.card}
        />
        <ChooseComponentCard
          name={'Gigabyte RTX 3080'}
          text={
            '8GB, здесь типо какие-то характеристики здесь типо какие-то характеристики, здесь типо какие-то харак'
          }
          price={100}
          img={videoCardImg}
          className={styles.card}
        />
        <Btn className={styles.button}>Показать еще...</Btn>
      </div>
    </div>
  );
};

export default AddWindow;
