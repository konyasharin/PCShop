package com.example.pc.dto;

import lombok.Data;
import org.springframework.web.multipart.MultipartFile;

@Data
public class VideoCardDto {

    private String brand;

    private String model;

    private Integer price;

    private String description;

    private String country;

    private Integer memory_db;

    private String memory_type;

    private MultipartFile image;
}
