package com.example.pc.IServices;

import com.example.pc.Models.VideoCardModel;
import com.example.pc.dto.VideoCardDto;

import java.util.List;

public interface IVideoCardService {

    List<VideoCardModel> getVideoCards();
    VideoCardModel getVideoCardById(Long id);
    VideoCardModel updateVideoCard(VideoCardModel videoCardModel);
    void deleteVideoCardById(Long id);
    VideoCardModel createVideoCard(VideoCardDto videoCardDto);
}
