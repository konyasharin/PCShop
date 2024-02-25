import { useState } from 'react';
import { TFilters } from 'store/slices/filtersSlice.ts';

function useFilters(filtersState: TFilters) {
  const [filters, setFilters] = useState<TFilters>({
    ...filtersState,
    checkBoxesBlocks: filtersState.checkBoxesBlocks.map(checkBoxesBlock => {
      return {
        ...checkBoxesBlock,
        checkBoxes: checkBoxesBlock.checkBoxes.map(checkBox => {
          return {
            ...checkBox,
            isActive: false,
          };
        }),
      };
    }),
    radiosBlocks: filtersState.radiosBlocks.map(radiosBlock => {
      return {
        ...radiosBlock,
        checkBoxes: radiosBlock.checkBoxes.map(checkBox => {
          return {
            ...checkBox,
            isActive: false,
          };
        }),
      };
    }),
  });

  function setCheckBoxIsActive(
    nameBlock: string,
    index: number,
    newIsActive: boolean,
  ) {
    setFilters({
      ...filters,
      checkBoxesBlocks: filters.checkBoxesBlocks.map(checkBoxBlock => {
        return {
          ...checkBoxBlock,
          checkBoxes: checkBoxBlock.checkBoxes.map((checkBox, i) => {
            if (nameBlock === checkBoxBlock.name && index === i) {
              return {
                ...checkBox,
                isActive: newIsActive,
              };
            }
            return {
              ...checkBox,
            };
          }),
        };
      }),
    });
  }

  function setRadioIsActive(nameBlock: string, index: number) {
    setFilters({
      ...filters,
      radiosBlocks: filters.radiosBlocks.map(radioBlock => {
        return {
          ...radioBlock,
          checkBoxes: radioBlock.checkBoxes.map((radio, i) => {
            if (nameBlock === radioBlock.name) {
              return {
                ...radio,
                isActive: (radio.isActive = i === index),
              };
            }
            return {
              ...radio,
            };
          }),
        };
      }),
    });
  }

  function setInputValue(name: string, newValue: string) {
    setFilters({
      ...filters,
      inputsBlocks: filters.inputsBlocks.map(inputsBlock => {
        return {
          ...inputsBlock,
          inputs: inputsBlock.inputs.map(input => {
            return {
              ...input,
              value: input.name === name ? newValue : input.value,
            };
          }),
        };
      }),
    });
  }

  return { filters, setCheckBoxIsActive, setRadioIsActive, setInputValue };
}

export default useFilters;
