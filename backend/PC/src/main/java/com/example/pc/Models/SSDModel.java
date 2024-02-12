package com.example.pc.Models;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

import javax.persistence.*;
import java.util.List;

@Entity
@Table(name="SSD")
@Data
@NoArgsConstructor
@AllArgsConstructor
public class SSDModel {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(name = "brand")
    private String brand;

    @Column(name="model")
    private String model;

    @Column(name="price")
    private Integer price;

    @Column(name="country")
    private String country;

    @Column(name="capacity")
    private Integer capacity;

    @OneToMany
    @JoinColumn(name = "image_id")
    private List<ImageModel> images;

    @Column(name = "preview")
    private byte[] preview;
}
