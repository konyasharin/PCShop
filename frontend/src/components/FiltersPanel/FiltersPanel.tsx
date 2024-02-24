import React from 'react';
import TComponentTypes from 'types/TComponentTypes.ts';

type FiltersPanelProps = TComponentTypes & {
  checkBoxesBlocks: React.ReactNode[];
  radiosBlocks: React.ReactNode[];
  inputsBlocks: React.ReactNode[];
};

const FiltersPanel: React.FC<FiltersPanelProps> = props => {
  return (
    <div>
      {...props.checkBoxesBlocks}
      {...props.radiosBlocks}
      {...props.inputsBlocks}
    </div>
  );
};

export default FiltersPanel;
