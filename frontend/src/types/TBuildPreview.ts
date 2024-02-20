type TBuildPreview = {
  img: string;
  className?: string;
  name: string;
  description: {
    videoCard: string;
    processor: string;
    RAM: string;
    cooling: string;
  };
};

export default TBuildPreview;
