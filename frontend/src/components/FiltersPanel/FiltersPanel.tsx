import CheckBoxesBlock from 'components/FiltersPanel/CheckBoxexBlock/CheckBoxesBlock.tsx';
import React from 'react';
import TComponentTypes from 'types/TComponentTypes.ts';
import { useSelector } from 'react-redux';
import { RootState } from 'store/store.ts';
import { filtersState } from 'store/slices/filtersSlice.ts';

const FiltersPanel: React.FC<TComponentTypes> = props => {
  const filters = useSelector((state: RootState) => {
    let filtersSelected: filtersState | undefined;
    state.filters.forEach(filterState => {
      if (filterState.type === props.type) {
        filtersSelected = filterState;
      }
    });
    if (!filtersSelected) {
      console.log(`Type ${props.type} not found in state`);
      return;
    }
    return filtersSelected;
  });
  const checkBoxes = filters
    ? filters.checkBoxes.map(checkBoxesObject => {
        return (
          <CheckBoxesBlock
            checkBoxes={checkBoxesObject.checkBoxes}
            title={checkBoxesObject.name}
          />
        );
      })
    : [];
  return <div>{...checkBoxes}</div>;
};

export default FiltersPanel;
