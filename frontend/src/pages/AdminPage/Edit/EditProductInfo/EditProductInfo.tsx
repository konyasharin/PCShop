import React, { ReactNode } from 'react';
import componentTypes from 'enums/componentTypes.ts';
import EditFilterBlock from '../EditFilterBlock/EditFilterBlock.tsx';
import Radio from 'components/Radio/Radio.tsx';
import styles from './EditProductInfo.module.css';
import Input from 'components/inputs/Input/Input.tsx';
import productCharacteristics from 'enums/characteristics/productCharacteristics.ts';
import { TComponentFilterKeys } from 'hooks/useAllFilters.ts';
import SelectImg from 'components/SelectImg/SelectImg.tsx';

type EditProductInfoProps = {
  type: keyof typeof componentTypes;
  price: number;
  setPrice: (newPrice: number) => void;
  img: string;
  imgFileRef: React.MutableRefObject<null | File>;
  setImg: React.Dispatch<React.SetStateAction<string>>;
  setDescription: (newValue: string) => void;
  description: string;
  setAmount: (newValue: number) => void;
  amount: number;
  power: number;
  setPower: (newValue: number) => void;
  filters: TComponentFilterKeys | null;
  setRadioIsActive: (
    nameBlock: keyof TComponentFilterKeys,
    index: number,
  ) => void;
};

const EditProductInfo: React.FC<EditProductInfoProps> = props => {
  const filtersBlocks: ReactNode[] = [];
  if (props.filters != null) {
    const filterKeys = Object.keys(
      props.filters,
    ) as (keyof typeof props.filters)[];
    filterKeys.forEach(filterKey => {
      const filterElements = props.filters![filterKey].map((filterElem, i) => {
        return (
          <Radio
            text={filterElem.text}
            isActive={filterElem.isActive}
            onChange={() => {
              props.setRadioIsActive(filterKey, i);
            }}
            className={styles.filterElement}
          />
        );
      });
      filtersBlocks.push(
        <EditFilterBlock
          filterBlock={
            <div className={styles.filterElements}>{...filterElements}</div>
          }
          title={productCharacteristics[filterKey]}
          className={styles.filterBlock}
        />,
      );
    });
  }

  return (
    <>
      <SelectImg
        img={props.img}
        setImg={imgFile => {
          props.setImg(URL.createObjectURL(imgFile));
          props.imgFileRef.current = imgFile;
        }}
        className={styles.img}
      />
      <EditFilterBlock
        title={'Цена'}
        filterBlock={
          <Input
            value={`${props.price}`}
            placeholder={'Цена товара'}
            onChange={newValue => props.setPrice(+newValue)}
            className={styles.priceInput}
            type={'number'}
          />
        }
        className={styles.filterBlock}
      />
      <EditFilterBlock
        title={'Описание'}
        filterBlock={
          <Input
            value={props.description}
            placeholder={'Описание товара'}
            onChange={newValue => props.setDescription(newValue)}
            className={styles.priceInput}
          />
        }
        className={styles.filterBlock}
      />
      <EditFilterBlock
        title={'Количество'}
        filterBlock={
          <Input
            value={`${props.amount}`}
            placeholder={'Количество товара'}
            onChange={newValue => props.setAmount(+newValue)}
            className={styles.priceInput}
            type={'number'}
          />
        }
        className={styles.filterBlock}
      />
      <EditFilterBlock
        title={'Мощность'}
        filterBlock={
          <Input
            value={`${props.power}`}
            placeholder={'Мощность компонента'}
            onChange={newValue => props.setPower(+newValue)}
            className={styles.priceInput}
            type={'number'}
          />
        }
        className={styles.filterBlock}
      />
      {...filtersBlocks}
    </>
  );
};

export default EditProductInfo;
