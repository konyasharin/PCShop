import React from 'react';
import TCheckBox from 'types/TCheckBox.ts';
import Radio from 'components/Radio/Radio.tsx';

type RadiosBlockProps = {
  radios: TCheckBox[];
  title: string;
  onChange: (nameBlock: string, index: number) => void;
  radioClassName?: string;
};

const RadiosBlock: React.FC<RadiosBlockProps> = props => {
  return (
    <>
      {...props.radios.map((radio, i) => {
        return (
          <Radio
            text={radio.text}
            isActive={radio.isActive}
            onChange={() => props.onChange(props.title, i)}
            className={props.radioClassName ? props.radioClassName : ''}
          />
        );
      })}
    </>
  );
};

export default RadiosBlock;
