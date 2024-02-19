package com.example.pc.Services;

import com.example.pc.IServices.IVideoCardService;
import com.example.pc.Models.VideoCardModel;
import com.example.pc.dto.VideoCardDto;
import com.example.pc.repositories.VideoCardRepo;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.io.IOException;
import java.util.List;

@Service
public class VideoCardService implements IVideoCardService {

    private final VideoCardRepo videoCardRepo;

    private static final Logger logger = LoggerFactory.getLogger(VideoCardService.class);

    @Autowired
    public VideoCardService(VideoCardRepo videoCardRepo) {
        this.videoCardRepo = videoCardRepo;
    }

    @Override
    public List<VideoCardModel> getVideoCards() {
        return videoCardRepo.findAll();
    }

    @Override
    public VideoCardModel getVideoCardById(Long id) {
        return videoCardRepo.findById(id).orElse(null);
    }

    @Override
    public VideoCardModel updateVideoCard(VideoCardModel videoCardModel) {
        return videoCardRepo.save(videoCardModel);
    }

    @Override
    public void deleteVideoCardById(Long id) {
        videoCardRepo.deleteById(id);
    }

    @Override
    public VideoCardModel createVideoCard(VideoCardDto videoCardDto){

        try {
            byte[] imageData = videoCardDto.getImage().getBytes();
            return videoCardRepo.save(VideoCardModel.builder()
                    .brand(videoCardDto.getBrand())
                    .model(videoCardDto.getModel())
                    .description(videoCardDto.getDescription())
                    .price(videoCardDto.getPrice())
                    .country(videoCardDto.getCountry())
                    .memory_db(videoCardDto.getMemory_db())
                    .memory_type(videoCardDto.getMemory_type())
                    .image(imageData)
                    .build());

        }catch (IOException e){

            logger.error("Failed to convert VideoCard image to byte array", e);
            throw  new RuntimeException("Failed to convert VideoCard image to byte array", e);
        }
    }
}
