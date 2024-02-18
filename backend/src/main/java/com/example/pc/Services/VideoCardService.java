package com.example.pc.Services;

import com.example.pc.IServices.IVideoCardSerivice;
import com.example.pc.Models.VideoCardModel;
import com.example.pc.repositories.VideoCardRepo;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class VideoCardService implements IVideoCardSerivice {

    private final VideoCardRepo videoCardRepo;

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
    public VideoCardModel addVideoCard(VideoCardModel videoCardModel) {
        return videoCardRepo.save(videoCardModel);
    }

    @Override
    public void deleteVideoCardById(Long id) {
        videoCardRepo.deleteById(id);
    }
}
