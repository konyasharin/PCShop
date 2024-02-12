package com.example.pc.Services;

import com.example.pc.Models.VideoCardModel;
import com.example.pc.repositories.VideoCardRepo;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class VideoCardService {

    private final VideoCardRepo videoCardRepo;

    @Autowired
    public VideoCardService(VideoCardRepo videoCardRepo) {
        this.videoCardRepo = videoCardRepo;
    }

    public List<VideoCardModel> getVideoCards() {
        return videoCardRepo.findAll();
    }

    public VideoCardModel getVideoCardById(Long id) {
        return videoCardRepo.findById(id).orElse(null);
    }

    public VideoCardModel saveVideoCard(VideoCardModel videoCardModel) {
        return videoCardRepo.save(videoCardModel);
    }

    public void deleteVideoCardById(Long id) {
        videoCardRepo.deleteById(id);
    }
}
