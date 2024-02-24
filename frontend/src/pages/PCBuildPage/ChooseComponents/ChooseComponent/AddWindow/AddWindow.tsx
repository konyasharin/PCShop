import FiltersPanel from 'components/FiltersPanel/FiltersPanel.tsx';
import React from 'react';
import TComponentTypes from 'types/TComponentTypes.ts';
import useFilters from 'hooks/useFilters.ts';
import { useSelector } from 'react-redux';
import { RootState } from 'store/store.ts';
import CheckBoxesBlock from 'components/CheckBoxexBlock/CheckBoxesBlock.tsx';
import RadiosBlock from 'components/RadiosBlock/RadiosBlock.tsx';
import InputsBlock from 'components/FiltersPanel/InputsBlock/InputsBlock.tsx';
import FilterBlock from 'components/FiltersPanel/FilterBlock/FilterBlock.tsx';
import styles from './AddWindow.module.css';

const AddWindow: React.FC<TComponentTypes> = props => {
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
    <FiltersPanel
      type={props.type}
      checkBoxesBlocks={checkBoxes}
      radiosBlocks={radiosBlocks}
      inputsBlocks={inputsBlocks}
    />
  );
};

export default AddWindow;
