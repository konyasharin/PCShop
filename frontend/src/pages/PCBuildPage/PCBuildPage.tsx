import Container from 'components/Container/Container.tsx';
import Scale from './Scale/Scale.tsx';
import BuildsError from './BuildsError/BuildsError.tsx';
import PowerOfBuild from './PowerOfBuild/PowerOfBuild.tsx';
import ChooseComponents from './ChooseComponents/ChooseComponents.tsx';
import Input from 'components/inputs/Input/Input.tsx';
import styles from './PCBuildPage.module.css';
import Btn from 'components/btns/Btn/Btn.tsx';
import useBuild from 'hooks/useBuild.ts';
import componentTypes from 'enums/componentTypes.ts';
import SelectImg from 'components/SelectImg/SelectImg.tsx';

function PCBuildPage() {
  const {
    components,
    setComponent,
    toggleError,
    progressOfBuild,
    price,
    power,
    errors,
    setIsActive,
    createBuild,
    name,
    setName,
    img,
    setImg,
    imgFileRef,
  } = useBuild();
  const errorsBlocks = errors.map(error => {
    return (
      <BuildsError
        type={error.errorType}
        title={componentTypes[error.componentType]}
        description={error.errorDescription}
      />
    );
  });
  return (
    <div className={styles.body}>
      <Container>
        <h2 className={styles.build}>Ваша сборка</h2>
        <Input
          className={styles.input}
          value={name}
          placeholder={'Название сборки'}
          onChange={(newBuildName: string) => setName(newBuildName)}
        />
        <SelectImg
          img={img}
          setImg={fileImg => {
            setImg(URL.createObjectURL(fileImg));
            imgFileRef.current = fileImg;
          }}
          className={styles.img}
        />
        <Scale className={styles.scale} percents={progressOfBuild} />
        {...errorsBlocks}
        <ChooseComponents
          components={components}
          setComponent={setComponent}
          setIsActive={setIsActive}
          key={0}
        />
        <PowerOfBuild power={power} price={`${price}$`} />
        <Btn className={styles.button} onClick={() => createBuild()}>
          Cоздать пк
        </Btn>
      </Container>
    </div>
  );
}

export default PCBuildPage;
